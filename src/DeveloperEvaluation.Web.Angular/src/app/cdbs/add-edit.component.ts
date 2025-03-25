import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first, map } from 'rxjs/operators';
import { AlertService } from '@app/_services';
import { CdbService } from '@app/_services/cdb.service';
import { Cdb } from '@app/_models/cdb';

@Component({ templateUrl: 'add-edit.component.html' })
export class AddEditComponent implements OnInit {
    formCdb!: FormGroup;
    id?: any;
    title!: string;
    loading = false;
    submitting = false;
    submitted = false;

    cdb!: Cdb;
    isAddAnexo: boolean = false;
    file?: File = undefined;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private cdbService: CdbService,
        private alertService: AlertService
    ) { 

    }

    ngOnInit(){
        this.LoadData();
    }

    editMode(){
        return this.route.snapshot.params['id'] != undefined;
    }

    LoadData() {
        this.id = this.route.snapshot.params['id'];

        if (this.id) 
        {
            this.title = 'CDB';            

            this.formCdb = this.formBuilder.group({
                id: ['', Validators.required],
                value: ['', Validators.required],
                months: ['', Validators.required],
                cdi: ['', Validators.required],
                tb: ['', Validators.required],
                grossValue: ['', Validators.required],
                taxPercentage: ['', Validators.required],
                taxAmount: ['', Validators.required],
                netValue: ['', Validators.required]
            });
            this.loading = true;
            this.cdbService
                .get(this.id)
                .pipe(first())
                .subscribe({
                    next: (x: any) => {
                        this.cdb = x.data;

                        this.formCdb.patchValue(this.cdb);
                        this.loading = false;
                    },
                    error: error => {
                        this.alertService.error(error);
                        this.loading = false;
                    }
                });
        }
        else
        {
            this.title = 'Create a new CDB';
            
            this.formCdb = this.formBuilder.group({
                id: [''],
                value: ['', Validators.required],
                months: ['', Validators.required],
                cdi: [''],
                tb: [''],
                grossValue: [''],
                taxPercentage: [''],
                taxAmount: [''],
                netValue: ['']
            });            
        }
    }

    get f() { return this.formCdb.controls; }

    onSubmit() {
        if (this.formCdb.invalid) {
            return;
        }

        if (!this.id)
            this.router.navigateByUrl('/cdbs');
        else
            this.LoadData();        
    }

    saveCdb() {
        if (this.formCdb.invalid) {
            return;
        }

        this.submitted = true;

        this.submitting = true;

        this.alertService.clear();

        return (this.id
            ? this.cdbService.put(this.formCdb.value)
            : this.cdbService.post(this.formCdb.value))
            .pipe(first())
            .subscribe({
                next: (response: any) => {
                    this.submitting = false;
                    this.cdb = response.data
                    this.alertService.success(response.message);
                    this.router.navigate(['../cdbs/list'], { relativeTo: this.route });
                },
                error: error => {
                    this.alertService.error(error);
                    this.submitting = false;
                }
            })
    }

    setIsAddAnexo() {
        this.isAddAnexo = !this.isAddAnexo;
        this.file = undefined;
    }

    onFileSelected(event: any) {
        if (event.target.files.length == 0){
            this.file = undefined;
        }
        else{
            this.file = event.target.files[0];

            let value = this.formCdb.value;

            this.formCdb.setValue(value);
        }
    }    

    back() {
        this.router.navigate(['../cdbs/list'], { relativeTo: this.route });
    }

    monthsArray()
    {
        var array = [];

        for (let index = 1; index <= 60; index++) {
            array.push(index);
        }

        return array;
    }
}