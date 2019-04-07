import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupService } from 'src/app/services/group.service';
import { ToastrService } from 'ngx-toastr';
import { group } from 'src/app/models/group';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-group-form',
  templateUrl: './group-form.component.html',
  styleUrls: ['./group-form.component.css']
})
export class GroupFormComponent implements OnInit {
  members: any[];
  group: group = {
    id: 0,
    name: '',
    members: []
  };
  saveMember = [];
  
  constructor(private route: ActivatedRoute,
    private router: Router,
    private groupService: GroupService,
    private toastrService :ToastrService)
    {
      route.params.subscribe(p => {
        this.group.id = +p['id'] || 0;
      });
    }

  ngOnInit() {
    if(this.group.id !=0){

      this.groupService.get(this.group.id).subscribe(g=>{
        this.setGroup(g)
      },
      err =>{
        this.toastrService.error('Page Not Found', 'Error', {
          timeOut: 3000
        });
        this.router.navigate(['/groups']);

    });

  
    this.populateMembers();
  }}

  private setGroup(v: group) {
    this.group.id = v.id;
    this.group.name = v.name;

    for(var i = 0; i<v.members.length; i++){
      this.saveMember.push(v.members[i]["userName"]);
    }
    this.group.members = v.members;
  }

   isUserExist(userName){
      for(var i = 0; i<this.group.members.length; i++){

        if(userName == this.group.members[i]["userName"]){
          return true;
        }
      }
      return false;

  }
  private populateMembers() {
    this.groupService.getMembers()
      .subscribe(result=>{
        this.members = result
      });
  }
  
  onMemberToggle(userName, $event) {
    if ($event.target.checked){
      this.saveMember.push(userName);
    }
    else {
      var index = this.group.members.indexOf(userName);
      this.saveMember.splice(index, 1);
    }
  }
  submit(){
    this.group.members = this.saveMember;
    var result$ =(this.group.id == 0)?this.groupService.create(this.group):this.groupService.update(this.group);
  
      result$.subscribe(t=>{
          this.toastrService.success('Group '+this.group.name+' Created Success', 'Success!');
          this.router.navigate(['/groups']);
        },
        err =>{
          this.toastrService.error('An unexpected error happened', 'Error', {
            timeOut: 3000
          });

        });
  }
}
