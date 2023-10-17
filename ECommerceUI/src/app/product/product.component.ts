import { Component, Input, NgModule } from '@angular/core';
import { Product } from '../models/models';
import { UtilityService } from '../services/utility.service';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})

export class ProductComponent {

  @Input() view : 'grid' | 'list' | 'currcartitem' | 'prevcartitem' = 'grid';

  @Input() product:Product = {
    productId: 0,
    title: '',
    description: '',
    price: 0,
    quantity: 0,
    categoryId: 1,
    category: '',
    subCategory: '',
    offerId: 1,
    offerTitle: '',
    discount: 0,
    imageName: ''
  }

  constructor(public utilityService: UtilityService) {}

}
