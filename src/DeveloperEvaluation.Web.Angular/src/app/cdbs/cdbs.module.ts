import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { CdbsRoutingModule } from './cdbs-routing.module';
import { LayoutComponent } from './layout.component';
import { ListComponent } from './list.component';
import { AddEditComponent } from './add-edit.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { PaginationComponent } from '@app/_components/pagination/pagination.component';



@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        CdbsRoutingModule,
        NgxPaginationModule,
    ],
    declarations: [
        LayoutComponent,
        ListComponent,
        AddEditComponent,
        PaginationComponent
    ]
})
export class CdbsModule { }