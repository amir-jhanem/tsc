import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TicketService } from 'src/app/services/ticket.service';
import { SaveTicket } from 'src/app/models/ticket';
import { GroupService } from 'src/app/services/group.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-ticket-view',
  templateUrl: './ticket-view.component.html',
  styleUrls: ['./ticket-view.component.css']
})
export class TicketViewComponent implements OnInit {
  ticket: SaveTicket;
  groups: [];
  ticketId: number; 
  selectedGroup: number; 
  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private ticketService: TicketService,
    private groupService: GroupService,
    private toastrService:ToastrService,
    private auth :AuthService) {
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
      this.getGroups();
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.ticketService.delete(this.ticket.id)
        .subscribe(x => {
          this.router.navigate(['/tickets']);
        });
    }
  }

  getGroups(){
    this.groupService.getAll({
      pageSize: 50
    }).subscribe(result => {
      this.groups = result.items;
    });;
  }
  setSelectedGroup(groupId){
this.selectedGroup = groupId;
  }
  assignTicket(){
    console.log(this.selectedGroup);
    this.ticketService.assignTicket(this.ticketId,this.selectedGroup)
      .subscribe(res =>{
        this.toastrService.success('Assign Group Success', 'Success!');

        this.router.navigate(['/tickets']);
      });
  }
}
