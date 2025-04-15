import { MonthCdb } from "./monthCdb";

export class Cdb {
    constructor(
        public id?: number,
        public value?: number,
        public months?: number,
        public cdi?: number,
        public tb?: number,
        public monthCDBCollection?: MonthCdb[]) {
    }
}
