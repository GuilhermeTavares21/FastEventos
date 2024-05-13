import { EventoService } from '../../services/evento.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '../../models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {
modalRef?: BsModalRef;
isCollapsed = false;

public eventos: Evento[] = [];
public eventosFiltrados: Evento[] = [];

public widthImg: number = 85;
public marginImg: number = 2;
private _filtroLista: string = ''

public get filtroLista(){
  return this._filtroLista
}

public set filtroLista(value: string){
  this._filtroLista = value;
  this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos
}

public filtrarEventos(filtrarPor: string): Evento[]{
  filtrarPor = filtrarPor.toLocaleLowerCase();
  return this.eventos.filter(
    (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
    evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
  )
}

public getEventos():  void{
  this.eventoService.getEvento().subscribe({
    next: (_eventos: Evento[]) => {
      this.eventos = _eventos,
      this.eventosFiltrados = this.eventos
    },
    error: (error: any) => {
      this.spinner.hide();
      this.toastr.error('Erro ao carregar eventos','Error!');
    },
    complete: () => this.spinner.hide()

});

}

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {}
  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  }
  openModal(template: TemplateRef<void>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success("O Evento foi deletado","Evento deletado com sucesso!");
  }

  decline(): void {
    this.modalRef?.hide();
  }


}
