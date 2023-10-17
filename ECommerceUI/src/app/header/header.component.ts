import { Component, ElementRef, Type, ViewChild, ViewContainerRef } from '@angular/core';
import { Category, NavigationItems } from '../models/models';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { NavigationService } from '../services/navigation.service';
import { UtilityService } from '../services/utility.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  @ViewChild('modalTitle') modalTitle!: ElementRef;
  @ViewChild('container', { read: ViewContainerRef, static: true })
  container!: ViewContainerRef;
  navigationList: NavigationItems[] = [];
  cartItems:number=0;

  constructor(private navService:NavigationService,public utilService:UtilityService){}

  ngOnInit(){
    this.navService.getCategoryList().subscribe((res:any)=>{
   const list : Category[] = res.data;
      
      for(let item of list){
        
        let present = false;
        for(let navItem of this.navigationList){
          if(navItem.category === item.category){
            navItem.subcategories.push(item.subCategory);
            present = true;
          }
        }
        if(!present){
          this.navigationList.push({
            category:item.category,
            subcategories:[item.subCategory],
          }
          )
        }
      }
    });

    if(this.utilService.isLoggedIn()){
      this.navService.getActiveCartOfUser(Number(this.utilService.getUser()?.userId)).subscribe((res : any)=>{
        
        this.cartItems = res.anyData.cartItems.length;
      
      });
    }

    this.utilService.changeCart.subscribe((res:any)=>{
      if(parseInt(res) === 0) this.cartItems = 0;
      else this.cartItems += parseInt(res);
    })
  }

  openModal(name: string) {
    this.container.clear();
    let componentType! : Type<any>
    if(name === 'login'){
      componentType = LoginComponent;
      this.modalTitle.nativeElement.textContent = 'Enter Login Information';
    } 
    if(name === 'register'){
      componentType = RegisterComponent;
      this.modalTitle.nativeElement.textContent = 'Enter Registration Information';
    } 
    this.container.createComponent(componentType);
  }

}
