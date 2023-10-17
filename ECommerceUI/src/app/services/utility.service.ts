import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Cart, Payment, Product, User } from '../models/models';
import { Subject } from 'rxjs';
import { NavigationService } from './navigation.service';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  changeCart = new Subject();

  constructor(private jwt:JwtHelperService,private navService:NavigationService) { }

  applyDiscount(price:number,discount:number){
    let finalPrice:number = price - price * (discount/100);
    return finalPrice;
  }

  setUser(token :string){
    localStorage.setItem('user',token);
  }

  isLoggedIn(){
    return localStorage.getItem('user') ? true : false;
  }

  logoutUser(){
    localStorage.removeItem('user');
  }

  getUser() : User | null{
    let token = this.jwt.decodeToken();

    let user : User = {
      userId:token.UserId,
      firstName: token.FirstName,
      lastName: token.LastName,
      email: token.Email,
      address: token.Address,
      mobile: token.Mobile,
      password: '',
      createdAt: token.CreatedAt,
      modifiedAt: token.ModifiedAt
    };
    return user;
  }

  addToCart(product:Product){
    let productId = product.productId;
    
    let userId = Number(this.getUser()?.userId);

    this.navService.addToCart(userId,productId).subscribe((res:any)=>{
      if(res.success){
        this.changeCart.next(1);
      }
    })
  }

  calculatePayment(cart:Cart,payment:Payment){
    payment.totalAmount = 0;
    payment.amountPaid = 0;
    payment.amountReduced = 0;

    for(let cartItem of cart.cartItems){
      payment.totalAmount += cartItem.price

      payment.amountReduced += cartItem.price - this.applyDiscount(cartItem.price,cartItem.discount);

      payment.amountPaid += this.applyDiscount(cartItem.price,cartItem.discount);
    }

    if(payment.amountPaid > 50000){
      payment.shippingCharges = 2000;
    }
    else if(payment.amountPaid > 20000){
      payment.shippingCharges = 1000;
    }
    else if(payment.amountPaid > 5000){
      payment.shippingCharges = 500;
    }else{
      payment.shippingCharges = 200;
    }
  }

  calculatePricePaid(cart:Cart){
    let pricePaid = 0;
    for(let cartItem of cart.cartItems){
      pricePaid += this.applyDiscount(cartItem.price,cartItem.discount);
    }
    return pricePaid;
  }
}
