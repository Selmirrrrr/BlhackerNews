import { Injectable } from "@angular/core";
import { Http, Response, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/Rx";
import { INewsResponse } from "../models/INewsResponse";

@Injectable()
export class NewsService {

    private _postsURL = "/api/news";

    constructor(private http: Http) {
    }

    getPosts(pageNumber: number, pageSize: number): Observable<INewsResponse> {
        let params = new URLSearchParams();
        params.append('PageNumber', pageNumber.toString());
        params.append('PageSize', pageSize.toString());

        let options = new RequestOptions({ params: params });
        
        return this.http
            .get(this._postsURL, options)
            .map((response: Response) => {
                return <INewsResponse>response.json();
            })
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error.statusText);
    }
}