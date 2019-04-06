import { ActivatedRoute, Router } from '@angular/router';
import { TicketService } from 'src/app/services/ticket.service';
import { SaveTicket } from 'src/app/models/ticket';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-ticket-form',
  templateUrl: './ticket-form.component.html',
  styleUrls: ['./ticket-form.component.css']
})
export class TicketFormComponent implements OnInit {

  ticket: SaveTicket = {
    id: 0,
    subject: '',
    body: '',
    contact: {
      name: '',
      email: '',
    }
  };
  constructor(private route: ActivatedRoute,
    private router: Router,
    private ticketService: TicketService,
    private toastrService :ToastrService)
    {
      route.params.subscribe(p => {
        this.ticket.id = +p['id'] || 0;
      });
     }

  ngOnInit() {
  }

  submit(){
    this.ticketService.create(this.ticket)
        .subscribe(t=>{
          console.log(t);
          this.toastrService.success('Thanks for your request we will contanct with you by email', 'Success!');
          this.router.navigate(['/home']);
        },
        err =>{
          this.toastrService.error('An unexpected error happened', 'Error', {
            timeOut: 3000
          });

        });
  }

}
