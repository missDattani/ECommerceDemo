import { Component } from '@angular/core';
import { SuggestedProduct } from '../models/models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  SuggestedProducts : SuggestedProduct[] = [
    {
      bannerimage : 'Banner/Baner_Mobile.png',
      category: {
        categoryId: 0,
        category: 'electronics',
        subCategory: 'mobiles',
      },
    },
    {
      bannerimage : 'Banner/Baner_Laptop.png',
      category: {
        categoryId: 0,
        category: 'electronics',
        subCategory: 'laptops',
      },
    },
    {
      bannerimage : 'Banner/Baner_Chair.png',
      category: {
        categoryId: 0,
        category: 'furniture',
        subCategory: 'chairs',
      },
    },
  ];

}
