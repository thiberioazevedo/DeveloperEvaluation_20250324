import { Component, OnInit } from '@angular/core';
import { first, map } from 'rxjs/operators';

import { AccountService, AlertService } from '@app/_services';
import { CdbService } from '@app/_services/cdb.service';
import { PaginationComponent } from '@app/_components/pagination/pagination.component';
import { ApiResponseWithData } from '@app/_models/api-response-with-data';
import { Cdb } from '@app/_models/cdb';
import { MonthCdb } from '@app/_models/monthCdb';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    loading: boolean = false;
    apiResponseWithData!: ApiResponseWithData<Cdb>;
    constructor(private cdbService: CdbService, private alertService: AlertService) {}

    ngOnInit() {
        this.dataBind();        
    }

    dataBind() {
        this.loading = true;
        this.cdbService.list(this.apiResponseWithData?.data.pageNumber || 1, this.apiResponseWithData?.data.pageSize || 10, '', '', true)
            .pipe(first())
            .subscribe({
                next: (apiResponseWithData) => {
                    this.loading = false;
                    this.apiResponseWithData = apiResponseWithData;
                    return apiResponseWithData;
                },
                error: error => {
                    this.loading = false;
                    this.alertService.error(error);
                }
            });
    }

    deleteCdb(id: any) {
        this.loading = true;
        this.cdbService.delete(id)
            .pipe(first())
            .subscribe({
                next: () => {
                    this.alertService.success('Operação realizada com sucesso');
                    this.dataBind();
                    this.loading = false;
                },
                error: error => {
                    this.alertService.error(error);
                    this.loading = false;
                }
            });             
    }

    onPageChange(event: any){
        this.dataBind();
    }

    public getLastMonthCDB(cdb: Cdb): MonthCdb | undefined {
        return cdb.monthCDBCollection?.find(i => i.month === cdb.months)
    }

    getTotalTaxAmount(cdb: Cdb): number {
        return cdb?.monthCDBCollection?.reduce((acc, item) => acc + (item.taxAmount || 0), 0) || 0;
    }
}