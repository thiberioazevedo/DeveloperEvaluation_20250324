export class MonthCdb {
    constructor(
        public month: number,
        public initialValue: number,
        public finalValue: number,
        public grossValue?: number,
        public taxPercentage?: number,
        public taxAmount?: number,
        public netValue?: number) {
    }
}
