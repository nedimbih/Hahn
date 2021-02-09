export class Applicant {
    constructor(public id: Number = 0,
        public name: string = "",
        public familyName: string = "",
        public address: string = "",
        public countryOfOrigin: string = "",
        public emailAddress: string = "",
        public age: string = "",
        public hired: Boolean = false) { }
}