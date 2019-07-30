import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AddCustomer } from '../../models/user/dto/add-customer';
import { CustomerService } from '../../services/user/customer.service';


@Component({
  selector: 'app-shop-add-customer',
  templateUrl: './shop-add-customer.component.html'
})
export class ShopAddCustomerComponent implements OnInit {
  private apiError: string = "";
  private messageSuccess: string = "";
  private addCustomerForm: FormGroup;
  private addCustomerDto: AddCustomer = new AddCustomer();

  constructor(private customerService: CustomerService) {

  }

  ngOnInit() {
    this.addCustomerForm = new FormGroup({
      'email': new FormControl('', [Validators.required, Validators.email]),
      'name': new FormControl('', Validators.required),
      'lastname': new FormControl('', Validators.required),
      'surname': new FormControl(''),
      'sex': new FormControl(''),
      'phone': new FormControl('', Validators.pattern("^[0-9]{10}$")),
    });
  }

  public addCustomer() {
    this.addCustomerDto.Email = this.addCustomerForm.value.email;
    this.addCustomerDto.Name = this.addCustomerForm.value.name;
    this.addCustomerDto.LastName = this.addCustomerForm.value.lastname;
    this.addCustomerDto.SurName = this.addCustomerForm.value.surname;
    this.addCustomerDto.Sex = this.addCustomerForm.value.sex;
    this.addCustomerDto.Phone = this.addCustomerForm.value.phone;
    this.customerService.addCustomer(this.addCustomerDto).subscribe(() => {
      this.messageSuccess = "You added new customer!";
      this.apiError = "";
    }, err => {
      this.apiError = err.error.Message;
      this.messageSuccess = "";
    });
  }

}
