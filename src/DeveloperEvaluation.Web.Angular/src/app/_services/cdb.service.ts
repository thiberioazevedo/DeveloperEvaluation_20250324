import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Cdb } from '@app/_models/cdb';
import { BaseService, StorangeEnum } from './base.service';

@Injectable({ providedIn: 'root' })
export class CdbService extends BaseService<Cdb> {
    private cdbSubject: BehaviorSubject<Cdb | null>;
    public cdb: Observable<Cdb | null>;

    constructor(
        private router: Router,
        httpClient: HttpClient,
    ) {
        super(httpClient, 'cdbs');
        this.cdbSubject = new BehaviorSubject(JSON.parse(localStorage.getItem(StorangeEnum.session.toString())!));
        this.cdb = this.cdbSubject.asObservable();
    }

    public get cdbValue() {
        return this.cdbSubject.value;
    }

    public override post(cdb: Cdb, route?: string){
        return super.post({
            Value: cdb.value,
            Months: cdb.months
            }, route);
    }

    // add(cdb: Cdb) {
    //     return this.post(cdb)
    // }

    // getAll() {
    //     return this.list()
    // }

    // getById(id: any) {
    //     return this.get(id)
    // }

    // update(id: number, boby: any) {
    //     return this.post(boby)
    //         .pipe(map(x => {
    //             return x;
    //         }));
    // }

    // override delete(id: any){
    //     return super.delete(id);
    // }

}