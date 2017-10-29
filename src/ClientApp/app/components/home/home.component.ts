import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NewsService } from "./../../services/NewsService";
import { INews } from "./../../models/INewsItem";
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    providers: [NewsService]
})
export class HomeComponent {
    asyncNews: Observable<INews[]>;
    p: number = 1;
    total: number;
    loading: boolean;

    constructor(private newsService: NewsService) {
    }

    getPage(page: number) {
        this.asyncNews = this.newsService.getPosts(page, 10)
        .do(res => {
            this.p = page;
        })
        .map(res => res.items)
        .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error.statusText);
    }

    ngOnInit(): void {
        this.getPage(1);
    }


    public PreviousPage() {
        this.getPage(this.p > 1 ? --this.p : this.p);
    }

    public NextPage() {
        this.getPage(this.p < 50 ? ++this.p : this.p);
    }

    public open(news:INews) {
        window.location.href = news.url;
    }
}