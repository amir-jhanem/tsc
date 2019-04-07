import { Component, OnInit } from '@angular/core';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-group-list',
  templateUrl: './group-list.component.html',
  styleUrls: ['./group-list.component.css']
})
export class GroupListComponent implements OnInit {
  private readonly PAGE_SIZE = 6; 

  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Group Name' },
    { title: 'Members Count' },
    { title: 'Total Assign Tickets' },
    { }
  ];

  constructor(private groupService: GroupService) { }

  ngOnInit() { 

    this.populateGroups();
  }

  private populateGroups() {
    this.groupService.getAll(this.query)
      .subscribe(result => {
        console.log(result);
        this.queryResult = result;
      });
  }

  onFilterChange() {
    this.query.page = 1; 
    this.populateGroups();
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.populateGroups();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending; 
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateGroups();
  }

  onPageChange(page) {
    this.query.page = page; 
    this.populateGroups();
  }
}