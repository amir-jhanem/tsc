import { TicketService } from 'src/app/services/ticket.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.css']
})
export class TicketListComponent implements OnInit {
  private readonly PAGE_SIZE = 6; 

  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Subject', key: 'subject', isSortable: true },
    { title: 'Ticket Date', key: 'date', isSortable: true },
    { title: 'Group', key: 'group', isSortable: true },
    { title: 'Status', key: 'status', isSortable: true },
    { }
  ];

  constructor(private ticketService: TicketService) { }

  ngOnInit() { 

    this.populateTickets();
  }

  private populateTickets() {
    this.ticketService.getAll(this.query)
      .subscribe(result => this.queryResult = result);
  }

  onFilterChange() {
    this.query.page = 1; 
    this.populateTickets();
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.populateTickets();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending; 
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateTickets();
  }

  onPageChange(page) {
    this.query.page = page; 
    this.populateTickets();
  }

}
