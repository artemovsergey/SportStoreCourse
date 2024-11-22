import { CanDeactivateFn } from '@angular/router';
import { HomeComponent } from '../components/home/home.component';
import { EditUserComponent } from '../components/edit-user/edit-user.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<EditUserComponent> = (component:EditUserComponent) => {

  if(component.editForm.dirty){
    return confirm("Вы хотите продолжить?")
  }

  return true;
};
