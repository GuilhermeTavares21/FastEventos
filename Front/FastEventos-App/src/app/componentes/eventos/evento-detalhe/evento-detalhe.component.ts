import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

form!: FormGroup

get f(): any {
  return this.form.controls;
}

public resetForm(): void {
  this.form.reset();
}

  constructor(
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.validation()
  }

    public validation(): void {


      this.form = this.fb.group({
        local: ['',
          [Validators.required]
        ],
        dataEvento: ['',
          [Validators.required]
        ],
        tema: ['',
          [Validators.required, Validators.minLength(3), Validators.maxLength(30)]
        ],
        qtdPessoas: ['',
          [Validators.required, Validators.minLength(1), Validators.maxLength(120000)]],
        telefone: ['',
          [Validators.required]
        ],
        email: ['',
          [Validators.required, Validators.email]
        ],
        imagemURL: ['',Validators.required],
      });
    }
}
