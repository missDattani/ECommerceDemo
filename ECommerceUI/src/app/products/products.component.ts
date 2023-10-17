import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavigationService } from '../services/navigation.service';
import { UtilityService } from '../services/utility.service';
import { Product } from '../models/models';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {

  products:Product[]=[];

  view : 'grid' | 'list' = 'list';
  sortby : 'default' | 'htl' | 'lth' = 'default';


  constructor(private activatedRoute:ActivatedRoute,private naviService:NavigationService,private utilityService:UtilityService){}

  ngOnInit(){
    this.activatedRoute.queryParams.subscribe((params : any)=>{
      let category = params.category;
      let subCategory = params.subcategory;

      if(category && subCategory){
        this.naviService.getSuggestedProducts(category,subCategory,10).subscribe((res : any)=>{
            this.products = res.data;
        })
      }
    });
  }

  sortByPrice(sortKey:string){
    this.products.sort((a,b)=>{
      if(sortKey === 'default'){
        return a.productId > b.productId ? 1 : -1
      }
      if(sortKey === 'htl'){
        return this.utilityService.applyDiscount(a.price,a.discount) > this.utilityService.applyDiscount(b.price,b.discount) ? -1 : 1
      
      }
      if(sortKey === 'lth'){
        return this.utilityService.applyDiscount(a.price,a.discount) > this.utilityService.applyDiscount(b.price,b.discount) ? 1 : -1

      }
      return 0;
    })
  }

}
