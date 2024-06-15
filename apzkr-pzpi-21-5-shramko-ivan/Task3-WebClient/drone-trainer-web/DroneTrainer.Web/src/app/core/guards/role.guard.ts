import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';

export const checkUserRole = (allowedRoles: string[]): CanActivateFn => {
  const fn: CanActivateFn = () => {
    const userRole = localStorage.getItem('role');

    if (userRole && allowedRoles.includes(userRole)) {
      return true;
    }

    return false;
  };

  return fn;
};
