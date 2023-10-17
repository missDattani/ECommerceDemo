import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NavigationService } from '../services/navigation.service';
import { UtilityService } from '../services/utility.service';
import { Cart, Orders, Payment, PaymentMethod } from '../models/models';
import { timer } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {

  selectPaymentMethodName='a';
  selectedPaymentMethod = new FormControl('0');
  paymentMethods : PaymentMethod[]=[];

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

  address = '';
  mobile = '';
  messageNew='';
  className ='';

  displaySpinner = false;

  constructor(private navService:NavigationService,public utilityService:UtilityService,private router:Router){}

  ngOnInit() : void {

    this.navService.getPaymentMethods().subscribe((res:any)=>{
      this.paymentMethods = res.data;
    })

    this.selectedPaymentMethod.valueChanges.subscribe((res : any) => {
      if(res === '0'){
        this.selectPaymentMethodName = '';
      }
      else{
        this.selectPaymentMethodName = res.toString();
      }
    })

    this.navService.getActiveCartOfUser(Number(this.utilityService.getUser()?.userId)).subscribe((res:any)=>{
      this.userCart = res.anyData
      this.utilityService.calculatePayment(this.userCart,this.userPaymentInfo);
    });

    this.address = String(this.utilityService.getUser()?.address);
    this.mobile = String(this.utilityService.getUser()?.mobile);
 
  }
  getpaymentMethod(id:string){
    let x = this.paymentMethods.find((v) => v.paymentMethodId === parseInt(id));
    return x?.type + '-' + x?.provider;
  }

  placeOrder(){
  
    this.displaySpinner = true;
    let isPaymentSuccessfull = this.payMoney();
    debugger
    if(!isPaymentSuccessfull){
      this.displaySpinner = false;
      this.messageNew = "Something went wrong";
      this.className = 'text-danger';
      return;
    }

    let step = 0;
    let counter = timer(0,3000).subscribe((res)=>{
      ++step;
      if(step == 1){
        this.messageNew = "Processing Payment";
        this.className = 'text-success';
      }
      if(step == 2){
        this.messageNew = "Payment successfull, order is being placed";
        this.storeOrder();
      }
      if(step == 3){
        this.messageNew = "Your Order has been placed";
        this.displaySpinner = false;
      }
      if(step == 4){
        this.router.navigateByUrl('/home');
        counter.unsubscribe();
      }
    });
  }

  payMoney(){
    return true;
  }

  storeOrder(){
    let payment : Payment;
    let pmid = 0;
    if(this.selectedPaymentMethod.value)
      pmid = parseInt(this.selectedPaymentMethod.value);

      payment = {
        id:0,
        paymentMethodId:pmid,
        type:'',
        provider:'',
        avilable:false,
        reason:'',
        userId:Number(this.utilityService.getUser()?.userId),
        totalAmount:this.userPaymentInfo.totalAmount,
        shippingCharges:this.userPaymentInfo.shippingCharges,
        amountReduced:this.userPaymentInfo.amountReduced,
        amountPaid:this.userPaymentInfo.amountPaid,
        createdAt:''
      }
    
    this.navService.insertPayment(payment).subscribe((res:any)=>{
      console.log(res);
      if(res.success === true){
        payment.id = parseInt(res.data);
        let order:Orders={
          id:0,
          userId:Number(this.utilityService.getUser()?.userId),
          cartId:this.userCart.cartId,
          paymentId:payment.id,
          createdAt:''
        }

        this.navService.insertOrder(order).subscribe((res)=>{
          debugger
          this.utilityService.changeCart.next(0);
        })
      }
    })
  }
}


