import { Component, OnInit, Input } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { MenuItem } from 'src/app/shared/models/shared-models';
import { Router } from '@angular/router';
import * as _ from 'lodash';

@Component({
  selector: 'app-shell-navlist',
  templateUrl: './shell-navlist.component.html',
  styleUrls: ['./shell-navlist.component.scss'],
  animations: [
    trigger('parentActive', [
      state('inactive', style({
        transform: 'rotate(0deg)'
      })),
      state('active', style({
        transform: 'rotate(180deg)'
      })),
      transition('inactive => active', animate('0ms ease-in')),
      transition('active => inactive', animate('0ms ease-out'))
    ]),
    trigger('childrenActive', [
      state('inactive', style({
        opacity: '0',
        display: 'none'
      })),
      state('active', style({
        opacity: '1'
      })),
      transition('inactive => active', animate('0ms ease-in')),
      transition('active => inactive', animate('0ms ease-out'))
    ])
  ]
})
export class ShellNavlistComponent implements OnInit {
  @Input() menuItems: MenuItem[];
  private activeParentMenuItems: string[] = [];
  constructor(private router: Router) { }

  ngOnInit() {
    this.initializeMenuItems();
  }
  toggleActive(menuItem: MenuItem) {
    if (this.isParentActive(menuItem) === 'active') {
      _.remove(this.activeParentMenuItems, x => x === menuItem.title);
    } else {
      this.activeParentMenuItems.push(menuItem.title);
    }
  }

  private isParentActive(menuItem: MenuItem) {
    const isActive = this.activeParentMenuItems.indexOf(menuItem.title) > -1;
    return isActive ? 'active' : 'inactive';
   }

  private initializeMenuItems() {
    for  (let i = 0; i < this.menuItems.length; i++) {
      if (this.parentOrChildIsActive(this.menuItems[i])) {
        this.activeParentMenuItems.push(this.menuItems[i].title);
      }
    }
  }

  private parentOrChildIsActive(menuItem: MenuItem) {
    if (this.router.isActive(menuItem.route, false)) {
      return true;
    }
    if (_.isNil(menuItem.children)) {
      return false;
    }

    for (let i = 0; i < menuItem.children.length; i++) {
      if (this.parentOrChildIsActive(menuItem.children[i])) {
        return true;
      }
    }
    return false;
  }
}
