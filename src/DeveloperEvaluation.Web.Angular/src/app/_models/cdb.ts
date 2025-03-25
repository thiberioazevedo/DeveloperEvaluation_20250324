import { MonthCdb } from "./monthCdb";

export class Cdb {
    constructor(
        public id?: number,
        public value?: number,
        public months?: number,
        public cdi?: number,
        public tb?: number,
        public grossValue?: number,
        public taxPercentage?: number,
        public taxAmount?: number,
        public netValue?: number,
        public monthCDBCollection?: MonthCdb[]) {
    }
}
