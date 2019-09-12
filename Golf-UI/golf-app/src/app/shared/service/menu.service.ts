import { Injectable } from '@angular/core';
import { MenuItem } from '../models/shared-models';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor() { }
  public  getMenuItems(): MenuItem[] {
    const items: MenuItem[] = [];
    const lande: MenuItem[] = [];
    items.push({
        title: 'Add New Game',
        route: '/dashboard/newGame',
        icon: ''
      });
    items.push({
        title: 'Join Game',
        route: '/dashboard/join',
        icon: ''
      });
    items.push({
        title: 'Logout',
        route: '',
        icon: 'exit_to_app'
      });
    return items;
}
}
