import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import "rxjs/Rx";
import { INewsResponse } from "../models/INewsResponse";
import { HttpParams, HttpClient, HttpResponse } from "@angular/common/http";

@Injectable()
export class NewsService {

    private _postsURL = "/api/news";

    constructor(private http: HttpClient) {
    }

    getPosts(pageNumber: number, pageSize: number): Observable<INewsResponse> {
        console.log(pageNumber);
        let params = new HttpParams();
        params = params.set('pageNumber', pageNumber.toString());
        params = params.set('pageSize', pageSize.toString());

        return this.http
            .get(this._postsURL, {params: params})
            .map((response: HttpResponse<INewsResponse>) => {
                return response;
            })
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error.statusText);
    }
}