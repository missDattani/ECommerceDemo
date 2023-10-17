import { Component, Input } from '@angular/core';
import { Category, Product } from '../models/models';
import { NavigationService } from '../services/navigation.service';

@Component({
  selector: 'app-suggested-product',
  templateUrl: './suggested-product.component.html',
  styleUrls: ['./suggested-product.component.css']
})
export class SuggestedProductComponent {
  @Input() category1 : Category = {
    categoryId: 0,
    category: '',
    subCategory: ''
  }

  @Input() categories: string = '';

  @Input() count: number = 3;
  products : Product[] = [];

  constructor(private navService:NavigationService){}

  ngOnInit() : void{
    this.navService.getSuggestedProducts(this.category1.category,this.category1.subCategory,this.count).subscribe((res:any) => {
      for(let product of res.data){
        this.products.push(product)
      }
    })
  }

}
