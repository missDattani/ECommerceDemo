import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Orders, Payment, PaymentMethod, User } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  baseUrl = 'https://localhost:7249/api/Shopping/';

  constructor(private http:HttpClient) { }

  getCategoryList(){
    return this.http.get<any[]>(this.baseUrl + "get-product-categories");
  }
   
  getSuggestedProducts(category:string,subCategory:string,count:number){
    return this.http.get<any[]>(this.baseUrl + "get-suggested-products",{
      params:new HttpParams()
      .set('category',category)
      .set('subCategory',subCategory)
      .set('count',count)
    });
  }

  getProductById(Id:number){
    return this.http.get(this.baseUrl + "get-product-by-id",{
      params:new HttpParams()
      .set('Id',Id)
    });
  }

  registerUser(user:User){
    return this.http.post(this.baseUrl + "register-user",user);
  }
  
  loginUser(email:string,password:string){
    return this.http.post(this.baseUrl + "login-user",{Email : email, Password : password});
  }

  submitReview(userId:number,productId:number,review:string){
    let obj : any ={
        userId:userId,
        productId:productId,
      review:review
    };
    return this.http.post(this.baseUrl + 'insert-review',obj);
  }

  getAllReviewOfProduct(productId:number){
    return this.http.get(this.baseUrl + "review-list",{
      params:new HttpParams()
      .set('ProductId',productId)
    });
  }

  addToCart(userId:number,productId:number){
    return this.http.post(this.baseUrl + "add-to-cart?userId=" + userId + "&ProductId=" + productId, null);
  }

  getActiveCartOfUser(UserId:number){
    return this.http.get(this.baseUrl + "get-active-cart",{
      params: new HttpParams()
      .set("UserId",UserId)
    });
  }

  getAllPreviousCartOfUser(userId: number){
    return this.http.get(this.baseUrl + "get-privious-carts",{
      params:new HttpParams()
      .set('UserId',userId)
    });
  }

  getPaymentMethods(){
    return this.http.get<PaymentMethod[]>(this.baseUrl + "get-payment-methods");
  }

  insertPayment(payment : Payment){
    debugger
    return this.http.post(this.baseUrl + "insert-payment",payment);
  }

  insertOrder(order:Orders){
    debugger
    return this.http.post(this.baseUrl + "insert-order",order);
  }
}
