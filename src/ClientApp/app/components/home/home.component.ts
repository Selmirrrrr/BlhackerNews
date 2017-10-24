import { Component } from '@angular/core';
import { NewsService } from "./../../services/NewsService";
import { INews } from "./../../models/INewsItem";

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    providers: [NewsService]
    
})
export class HomeComponent {
    _newsArray: INews[];
    
        constructor(private newsService: NewsService) {
        }
    
        getPosts(): void {
            this.newsService.getPosts()
                .subscribe(
                    resultArray => this._newsArray = resultArray,
                    error => console.log("Error :: " + error)
                )
        }
    
        ngOnInit(): void {
            this.getPosts();
        }
}
