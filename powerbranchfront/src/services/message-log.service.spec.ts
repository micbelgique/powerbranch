import { TestBed } from '@angular/core/testing';

import { MessageLogService } from './message-log.service';

describe('MessageLogService', () => {
  let service: MessageLogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MessageLogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
