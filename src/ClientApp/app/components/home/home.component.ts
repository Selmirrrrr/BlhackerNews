import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NewsService } from "./../../services/NewsService";
import { INews } from "./../../models/INewsItem";
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    providers: [NewsService]
})
export class HomeComponent {
    p: number = 1;
    asyncNews: Observable<INews[]>;
    total: number;
    loading: boolean;

    constructor(private newsService: NewsService) {
    }

    getPage(page: number) {
        this.loading = true;
        this.asyncNews = this.newsService.getPosts(this.p, 10)
            .do(res => {
                this.total = 500;
                this.p = page;
                this.loading = false;
            })
            .map(res => res.items);
    }

    ngOnInit(): void {
        this.getPage(1);        
    }

    public open(news:INews) {
        window.location.href = news.url;
    }
}
