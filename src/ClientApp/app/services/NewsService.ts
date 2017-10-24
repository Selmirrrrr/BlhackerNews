import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/Rx";
import { INews } from "./../models/INewsItem";

@Injectable()
export class NewsService {

    private _postsURL = "/api/news";

    constructor(private http: Http) {
    }

    getPosts(): Observable<INews[]> {
        return this.http
            .get(this._postsURL)
            .map((response: Response) => {
                return <INews[]>response.json();
            })
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error.statusText);
    }
}