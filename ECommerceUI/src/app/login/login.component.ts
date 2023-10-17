import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NavigationService } from '../services/navigation.service';
import { UtilityService } from '../services/utility.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm! : FormGroup;
  messageNew = '';

  constructor(private fb:FormBuilder,private navService:NavigationService,private utilityService:UtilityService){}

  ngOnInit(){
    this.loginForm = this.fb.group({
      email: ['',[Validators.required,Validators.email]],
      pwd: ['',[Validators.required,Validators.minLength(6),Validators.maxLength(15)]],
    });
  }

  get Email() : FormControl{
    return this.loginForm.get('email') as FormControl;
  }

  get PWD() : FormControl{
    return this.loginForm.get('pwd') as FormControl;
  }

  login(){
      this.navService.loginUser(this.Email.value,this.PWD.value).subscribe((res:any)=>{
      if(res.success !== false){
        this.messageNew = res.message;
        this.utilityService.setUser(res.token.toString());
        // console.log(res.data);
        console.log(this.utilityService.getUser());
        
      }else{
        this.messageNew = res.message;
      }
   
    })
  }

}
