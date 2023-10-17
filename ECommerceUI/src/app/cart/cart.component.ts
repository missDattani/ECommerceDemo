import { Component } from '@angular/core';
import { Cart, Payment } from '../models/models';
import { UtilityService } from '../services/utility.service';
import { NavigationService } from '../services/navigation.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {

  userCart:Cart={
    cartId:0,
    user:this.utilityService.getUser(),
    cartItems:[],
    ordered:false,
    orderedOn:''
  };

  userPaymentInfo : Payment = {
    id : 0,
    userId:Number(this.utilityService.getUser()?.userId),
    paymentMethodId:0,
    type:'',
    provider:'',
    avilable:false,
    reason:'',
    totalAmount:0,
    shippingCharges:0,
    amountReduced:0,
    amountPaid:0,
    createdAt:''
  }

  usersPreviousCart:Cart[]=[];

  constructor(public utilityService:UtilityService,private navService:NavigationService){}

  ngOnInit(){
    this.navService.getActiveCartOfUser(Number(this.utilityService.getUser()?.userId)).subscribe((res:any)=>{
      this.userCart = res.anyData
      this.utilityService.calculatePayment(this.userCart,this.userPaymentInfo);
    });

    this.navService.getAllPreviousCartOfUser(Number(this.utilityService.getUser()?.userId)).subscribe((res:any)=>{
      this.usersPreviousCart = res.data;
      console.log(this.usersPreviousCart);
      
    });

  }

}
