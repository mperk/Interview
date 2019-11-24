import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[appHighlight]'
})
export class HighlightDirective {
  htmlTag: HTMLElement = document.getElementsByTagName('html')[0];
  constructor(private el: ElementRef) { }

  @Input() defaultColor: string;

  @Input('appHighlight') highlightColor: string;

  @HostListener('click') onMouseClick() {
    if (this.el.nativeElement.localName == 'p') {
      this.clear();
      this.highlight(this.highlightColor || this.defaultColor || '#D7DAD8');
    }
  }

  // @HostListener('mouseenter') onMouseEnter() {
  //   this.highlight(this.highlightColor || this.defaultColor || '#FF5722');
  // }

  // @HostListener('mouseleave') onMouseLeave() {
  //   this.highlight(null);
  // }

  private highlight(color: string) {
    if (this.el.nativeElement.localName == 'p') {
      this.el.nativeElement.style.backgroundColor = color;
    }
  }

  private clear() {
    const nodes = $('.mat-tree li .text');
    for (let element of nodes) {
      element.setAttribute("style", "background-color:''")
    }
  }
}