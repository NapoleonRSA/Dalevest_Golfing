import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuItem } from '../shared/models/shared-models';
import { MenuService } from '../shared/service/menu.service';

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.scss']
})
export class ShellComponent implements OnInit {
  username: string;
  farmerId: string;
  showMenu = true;
  menuItems: MenuItem[] = [];
  constructor(private route: Router, private menuService: MenuService) {
    this.menuItems = [];
   }

  ngOnInit() {
    this.menuItems = this.menuService.getMenuItems();
  }

 logOut() {
    this.route.navigate(['']);
    localStorage.clear();
 }

 closeSideNav() {
   this.showMenu = true;
 }
}
