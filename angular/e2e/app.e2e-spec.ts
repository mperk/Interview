import { InterviewTemplatePage } from './app.po';

describe('Interview App', function() {
  let page: InterviewTemplatePage;

  beforeEach(() => {
    page = new InterviewTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
