import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '@app/models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { EventoService } from '@app/services/evento.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {
  modalRef?: BsModalRef;
  isCollapsed = false;

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  public widthImg: number = 120;
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
      private spinner: NgxSpinnerService,
      private router: Router
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

    detalharEvento(id: number):void {
      this.router.navigate([`eventos/detalhe/${id}`])
    }


}
