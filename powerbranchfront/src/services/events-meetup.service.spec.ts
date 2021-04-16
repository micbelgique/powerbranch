import { TestBed } from '@angular/core/testing';

import { EventsMeetupService } from './events-meetup.service';

describe('EventsMeetupService', () => {
  let service: EventsMeetupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventsMeetupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
