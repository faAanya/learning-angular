import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import { Auth } from '../services/auth';

@Directive({
  selector: '[appHiddenElements]'
})
export class HiddenElements implements OnInit{
  @Input("appHiddenElements") claimReq!: Function;

  constructor(
    private authService: Auth,
    private elementRef: ElementRef
  ) { }

  ngOnInit(): void {
    const claims = this.authService.getClaims();
    if(!this.claimReq(claims)){
      this.elementRef.nativeElement.style.display = "none";
    }
  }

}
