import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { HomeComponent, Item } from './home.component';
import { FilterPipe } from './filter.pipe';

describe('CounterComponent', () => {
  let fixture: ComponentFixture<HomeComponent>;
  let component: HomeComponent

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
    component = fixture.componentInstance;
  });

  it('should display title newest stories', (() => {
    const titleText = fixture.nativeElement.querySelector('h1').textContent;
    expect(titleText).toEqual('Newest Stories');
  }));

  it('should create HomeComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should create isLoading as true', () => {
    expect(component.isLoading).toBeTruthy();
  });

  it('should create hasMore as true', () => {
    expect(component.hasMore).toBeTruthy();
  });
  it('should create pageSize = 10', () => {
    expect(component.pageSize).toBe(10);
  });
  it('should create newestStories with lenght 0', () => {
    expect(component.newestStories.length).toBe(0);
  });


  it('should update page and hasMore when prevPage is called', () => {
    component.page = 10;
    component.itemsPerPage = 10;
    component.newestStories = Array.from({ length: 20 }, (_, i) => ({ title: `Item ${i}`, url: '' }));
    
    component.prevPage();
    
    expect(component.page).toBe(0);
    expect(component.hasMore).toBe(true);
  });

  it('should update page and search when onSearchTitle is called', () => {
    component.page = 10;
    component.search = '';
    
    component.onSearchTitle('Test Search');
    
    expect(component.page).toBe(0);
    expect(component.search).toBe('Test Search');
  });

});
