import { Component, OnInit } from '@angular/core';
import { TokenStorageService } from '../services/token-storage.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  currentUser: any;


  constructor(private token: TokenStorageService, private userService: UserService) { }


  ngOnInit(): void {
    this.currentUser = this.token.getUser();

    this.userService.setUser(this.currentUser);
  }
}
