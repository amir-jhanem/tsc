import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TicketService } from 'src/app/services/ticket.service';
import { SaveTicket } from 'src/app/models/ticket';

@Component({
  selector: 'app-ticket-view',
  templateUrl: './ticket-view.component.html',
  styleUrls: ['./ticket-view.component.css']
})
export class TicketViewComponent implements OnInit {
  ticket: SaveTicket;
  ticketId: number; 
  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private ticketService: TicketService) {
    route.params.subscribe(p => {
      this.ticketId = +p['id'];
      if (isNaN(this.ticketId) || this.ticketId <= 0) {
        router.navigate(['/tickets']);
        return; 
      }
    });
   }

  ngOnInit() {
    this.ticketService.get(this.ticketId)
    .subscribe(
      t => {
        console.log(t);
        this.ticket = t;
      },
      err => {
        if (err.status == 404) {
          this.router.navigate(['/tickets']);
          return; 
        }
      });
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.ticketService.delete(this.ticket.id)
        .subscribe(x => {
          this.router.navigate(['/tickets']);
        });
    }
  }
}
