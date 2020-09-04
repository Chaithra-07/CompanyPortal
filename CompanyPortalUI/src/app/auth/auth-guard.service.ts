import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { AuthService } from "./auth.service";
import { Injectable } from "@angular/core";
@Injectable()

export class AuthGuard implements CanActivate
{
constructor(private authService:AuthService){}
//Any time a component is added or removed or parameter is updated, a new snapshot is created.
canActivate(route:ActivatedRouteSnapshot,state:RouterStateSnapshot)
{
    return this.authService.isAuthenticated();
}
}