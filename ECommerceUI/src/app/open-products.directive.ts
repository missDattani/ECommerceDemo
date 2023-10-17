import { Directive, HostListener, Input } from '@angular/core';
import { Category } from './models/models';
import { Router } from '@angular/router';

@Directive({
  selector: '[appOpenProducts]'
})
export class OpenProductsDirective {

  @Input() category: Category={
    categoryId:0,
    category:'',
    subCategory:''
  };

@HostListener('click') openProducts(){
  this.router.navigate(['/products'],{
    queryParams:{
      category: this.category.category,
      subcategory: this.category.subCategory
    },
  });
}

  constructor(private router:Router) { }

}
