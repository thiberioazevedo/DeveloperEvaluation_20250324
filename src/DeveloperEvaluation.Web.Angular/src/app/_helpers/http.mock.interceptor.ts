import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import * as authResponse from './responsesFake/auth.response.json'
import * as createCdbResponse from './responsesFake/create-cdb.response.json'
import * as getCdbResponse from './responsesFake/create-cdb.response.json'
import * as listCdbsResponse from './responsesFake/list-cdbs.response.json'
import * as deleteCdbResponse from './responsesFake/delete-cdb.response.json'

const httpMockInterceptorEnabled = false;
const urls = [
    {
        url: 'api/auth',
        verb: 'post',
        json: authResponse
    },    
    {
        url: 'api/cdbs',
        verb: 'put',
        json: createCdbResponse
    },
    {
        url: 'api/cdbs/',
        verb: 'get',
        json: getCdbResponse
    },
    {
        url: 'api/cdbs',
        verb: 'get',
        json: listCdbsResponse
    },
    {
        url: 'api/cdbs?',
        verb: 'get',
        json: listCdbsResponse
    },    
    {
        url: 'api/cdbs/',
        verb: 'delete',
        json: deleteCdbResponse
    },
    {
        url: 'api/cdbs',
        verb: 'post',
        json: createCdbResponse
    },
];

@Injectable()
export class HttpMockInterceptor implements HttpInterceptor {
    constructor(private injector: Injector) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        for (const element of urls) {
            if (httpMockInterceptorEnabled &&request.method.toLowerCase() == element.verb.toLowerCase() && this.compare(this.getPathRelative(request.url), element.url))
            {
                console.log('Json loaded from HttpMockInterceptor : ' + request.url);
                return of(new HttpResponse({ status: 200, body: ((element.json) as any).default }));
            }
        }

        console.log('Loaded from http call :' + request.url);

        return next.handle(request);
    }

    getPathRelative(url: string){
        return url.split('/').map(i => i).filter((_, index) => index > 2).join('/').toLowerCase();
    }

    getPathBase(url: string){
        return url.split('/').map(i => i).filter((_, index) => index <= 2).join('/').toLowerCase();
    }

    compare(url: string, url2: string)  {
        if (url2.endsWith('?') || url2.endsWith('/')){
            return url.toLowerCase().startsWith(url2.toLowerCase());
        } else{
            return url.toLowerCase() == url2.toLowerCase();
        }
    }
}
