import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  form!: FormGroup;

  public resetForm(): void {
    this.form.reset();
  }

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.validation()
  }

  public validation(): void {
    this.form = this.fb.group({
      titulo: ['', Validators.required],
      primeiroNome:['', Validators.required],
      ultimoNome:['', Validators.required],
      email:['',
       [Validators.required, Validators.email]
      ],
      telefone:['', Validators.required],
      funcao:['', Validators.required],

    })
  }

}
