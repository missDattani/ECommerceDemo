import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavigationService } from '../services/navigation.service';
import { UtilityService } from '../services/utility.service';
import { Category, Product, ReviewData, SuggestedProduct, User } from '../models/models';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {

  ImageIndex : number = 1;
  product!:Product;
  reviewControl = new FormControl('');
  showError = false;
  suggestedProducts : string ='';
  reviewSaved = false;
  user!:User;
  otherReviews : ReviewData[] = [];

 

  constructor(private activatedRoute:ActivatedRoute,private naviService:NavigationService,public utilityService:UtilityService){}

  ngOnInit(){
    this.activatedRoute.queryParams.subscribe((params:any)=>{
      let productId = params.id;
      this.naviService.getProductById(productId).subscribe((res:any)=>{
        this.product=res.anyData;
        this.fetchAllReview();
      })
    })
  }

  submitReview(){
    let review = this.reviewControl.value;
    if(review === '' || review === null){
      this.showError = true;
      return;
    }

    let userId = Number(this.utilityService.getUser()?.userId);
    let productId = this.product.productId;

    this.naviService.submitReview(userId,productId,review).subscribe((res)=>{
    console.log(res);
      this.reviewSaved = true;
      this.fetchAllReview();
      this.reviewControl.setValue("");
    })
  }

  fetchAllReview(){
    this.otherReviews = [];
    this.naviService.getAllReviewOfProduct(this.product.productId).subscribe((res : any)=>{
      console.log(res);
      for(let review of res.data){
        this.otherReviews.push(review);
      }
    })
  }
}
