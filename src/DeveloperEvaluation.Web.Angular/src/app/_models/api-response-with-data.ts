import { PaginatedList } from "./paginated-list";

export class ApiResponseWithData<T> implements IApiResponseWithData {
    public data!: PaginatedList<T>;
    public success!: boolean;
    public message!: string;
    public errors!: []
}

export interface IApiResponseWithData{
    data: IPaginatedList;
}

export interface IPaginatedList{
    totalPages?: number
    totalCount?: number
    hasPrevious?: boolean
    hasNext?: boolean
    pageNumber?: number
    pageSize?: number
    collection?: any[]
}
