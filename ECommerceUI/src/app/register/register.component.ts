import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from '../models/models';
import { NavigationService } from '../services/navigation.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  inValidRPWD:boolean=false;
  registerForm!: FormGroup;
  messageNew = '';

  constructor(private fb:FormBuilder,private navService:NavigationService){}

  ngOnInit(){
    this.registerForm = this.fb.group({
      firstName : ['',[Validators.required,Validators.minLength(2),Validators.pattern('[a-zA-Z].*')]],
      lastName : ['',[Validators.required,Validators.minLength(2),Validators.pattern('[a-zA-Z].*')]],
      email: ['',[Validators.required,Validators.email]],
      address:['',[Validators.required]],
      mobile:['',[Validators.required]],
      pwd: ['',[Validators.required,Validators.minLength(6),Validators.maxLength(15)]],
      rpwd: [''],
    });
  }

  register(){
    let user : User = {
      userId:0,
      firstName: this.FirstName.value,
      lastName: this.LastName.value,
      email: this.Email.value,
      address:this.Address.value,
      mobile: this.Mobile.value,
      password: this.PWD.value,
      createdAt: "",
      modifiedAt: "",
    }

    this.navService.registerUser(user).subscribe((res : any) => {
      console.log(res.message);
      this.messageNew = res.message.toString();
      
    });
  }

  get FirstName() : FormControl{
    return this.registerForm.get('firstName') as FormControl;
  }

  get LastName() : FormControl{
    return this.registerForm.get('lastName') as FormControl;
  }

  get Email() : FormControl{
    return this.registerForm.get('email') as FormControl;
  }

  get Address() : FormControl{
    return this.registerForm.get('address') as FormControl;
  }

  get Mobile() : FormControl{
    return this.registerForm.get('mobile') as FormControl;
  }

 
  get PWD() : FormControl{
    return this.registerForm.get('pwd') as FormControl;
  }

  get RPWD() : FormControl{
    return this.registerForm.get('rpwd') as FormControl;
  }

}
