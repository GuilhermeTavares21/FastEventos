import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  form!: FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.validation
  }

    public validation(): void {
      this.form = new FormGroup({
        local: new FormControl('',Validators.required),
        dataEvento: new FormControl('',Validators.required),
        tema: new FormControl('',
        [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
        qtdPessoas: new FormControl('',
        [Validators.required, Validators.minLength(1), Validators.maxLength(120000)]),
        telefone: new FormControl('',Validators.required),
        email: new FormControl('',
        [Validators.required, Validators.email]),
        imagemURL: new FormControl('',Validators.required),
      });
    }
}
