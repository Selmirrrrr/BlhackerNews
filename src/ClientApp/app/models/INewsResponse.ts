import { INews } from "./INewsItem";
import { IPagingHeader } from "./IPagingHeader";

export interface INewsResponse {
    items: INews[];
    paging: IPagingHeader;
}