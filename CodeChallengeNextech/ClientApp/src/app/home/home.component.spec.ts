import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { HomeComponent } from './home.component';
import { FilterPipe } from './filter.pipe';

describe('CounterComponent', () => {
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach((() => {
    TestBed.configureTestingModule({
        imports: [HttpClientTestingModule], 
        providers: [HomeComponent,{provide:'BASE_URL',useValue:"https://localhost:44416/"}],
        declarations: [ HomeComponent, FilterPipe ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    fixture.detectChanges();
  });

  it('should display title newest stories', (() => {
    const titleText = fixture.nativeElement.querySelector('h1').textContent;
    expect(titleText).toEqual('Newest Stories');
  }));

//   it('should start with count 0, then increments by 1 when clicked', async(() => {
//     const countElement = fixture.nativeElement.querySelector('strong');
//     expect(countElement.textContent).toEqual('0');

//     const incrementButton = fixture.nativeElement.querySelector('button');
//     incrementButton.click();
//     fixture.detectChanges();
//     expect(countElement.textContent).toEqual('1');
//   }));
});
