import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmployeesService } from '../../services/employees.service';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { IEmployee } from '../../interfaces/employee.interface';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule],
  providers: [EmployeesService],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.scss'
})
export class EmployeesComponent implements OnInit {

  employees!: IEmployee[]
  showModal: boolean = false;
  actionEmployee: 'crear' | 'eliminar' | 'editar' | '' = '';
  employeeActive!: number;
  formEmployee!: FormGroup;
  imgEmployee!: string;

  constructor( private employeeService: EmployeesService, private formBuilder: FormBuilder ) {
    this.initFormEmployee();
  }

  ngOnInit(): void {
    this.getAllEmployees();
  }

  initFormEmployee() {
    this.formEmployee = this.formBuilder.group({
      cedula: [],
      nombre: [''],
      foto: [],
      fechaIngreso: [''],
      jobTitleId: []
    });
  }

  async getAllEmployees() {
    this.employees = await firstValueFrom( this.employeeService.getAllEmployees() );
  }

  async getEmployee(id: number) {
    const employee = await firstValueFrom( this.employeeService.getEmployee(id) );
    return employee;
  }

  setFormEmployee( employee: IEmployee ) {
    this.formEmployee.controls['cedula'].setValue(employee.cedula);
    this.formEmployee.controls['nombre'].setValue(employee.nombre);
    this.formEmployee.controls['fechaIngreso'].setValue(new Date(employee.fechaIngreso).toISOString().split('T')[0]);
    this.formEmployee.controls['jobTitleId'].setValue(employee.jobTitleId);
  }

  async createEmployee() {
    const newEmployee = {
      ...this.formEmployee.value,
      foto: this.imgEmployee
    }
    const createdEmployee = await firstValueFrom( this.employeeService.createEmployee(newEmployee) );
    this.showModal = false;
    this.getAllEmployees();
    this.formEmployee.reset();
    return createdEmployee;
  }

  async updateEmployee(id: number) {
    if( !this.formEmployee.value.foto ) delete this.formEmployee.value.foto;

    const currentEmployee = await this.getEmployee(id);
    const updatedEmployee: IEmployee = {
      ...currentEmployee,
      ...this.formEmployee.value,
      foto: this.imgEmployee
    } 
    
    const updateEmployee = await firstValueFrom( this.employeeService.updateEmployee(id, updatedEmployee) );
    this.getAllEmployees();
    this.formEmployee.reset();
    this.showModal = false;
    return updateEmployee;
  }

  async deleteEmployee(id: number) {
    const deleteEmployee = await firstValueFrom( this.employeeService.deleteEmployee(id) );
    this.getAllEmployees();
    this.showModal = false;
    return deleteEmployee;
  }

  async onFileSelected( event: any ) {
    if (event.target && event.target.files && event.target.files.length > 0) {
      const formData = new FormData();
      formData.append('file', event.target.files[0]);
      const imgBinary = await firstValueFrom( this.employeeService.getDataBinaryImg(formData) );
      this.imgEmployee = imgBinary.toString();
    }
  }

}
