<template>
  <form action="">
    <div class="input-section">
      <input
        class="input-field input-field__name"
        type="text"
        placeholder="Введите фамилию"
        v-model="postBody.firstName"
      />
      <input
        class="input-field input-field__name"
        type="text"
        placeholder="Введите имя"
        v-model="postBody.lastName"
      />
      <input
        class="input-field input-field__name"
        type="text"
        placeholder="Введите отчество"
        v-model="postBody.middleName"
      />
      <input
        class="input-field input-field__name"
        type="text"
        placeholder="Введите СНИЛС"
        v-model="postBody.SNILS"
      />
    </div>
    <section class="select">
      <div class="date-birthday">
        <p>Дата рождения</p>
        <input type="date" v-model="postBody.birthDate">
        <img src="../icons/calendar-im.png" alt="календарь">
      </div>
      <div class="gender">
        <p class="gender">Выберите пол</p>
        <input type="radio" id="one" value="Мужской" v-model="postBody.gender">
        <label for="one">Мужской</label>
        <input type="radio" id="two" value="Женский" v-model="postBody.gender">
        <label for="two">Женский</label>
      </div>
    </section>
    <button
      type="button"
      class="button-submit"
      @click="checkForm"
    >
      Добавить
    </button>

    <p class="errors" v-if="errors.length">
      <b>Пожалуйста, исправьте указанные ошибки:</b>
      <ul>
        <li v-for="error in errors">{{ error }}</li>
      </ul>
    </p>
  </form>
</template>

<script>
  import axios from "axios";

  export default {
    name: "CreatePatients",
    data() {
      return {
        errors: [],
        postBody: {
          "firstName": null,
          "lastName": null,
          "middleName": null,
          "birthDate": null,
          "gender": null,
          "SNILS": null
        },
      }
    },
    methods: {
      checkForm() {
        this.errors = [];

        if (!this.postBody.firstName) {
          this.errors.push('Укажите фамилию');
        }

        if (!this.postBody.lastName) {
          this.errors.push('Укажите имя');
        }

        if (!this.postBody.middleName) {
          this.errors.push('Укажите отчество');
        }

        if (!this.postBody.birthDate) {
          this.errors.push('Укажите дату рождения');
        }

        if (!this.postBody.gender) {
          this.errors.push('Выберите пол');
        }

        if (!this.postBody.SNILS) {
          this.errors.push('Введите СНИЛС');
        }

        if (this.postBody.SNILS !== null) {
          this.checkSNILS(this.postBody.SNILS);
        }

        if (!this.errors.length) {
          this.sendData();
        }
      },
      checkSNILS(snils) {
        const regExp = /^(\d{3})-(\d{3})-(\d{3})(\s{1})(\d{2})$/;
        if (!regExp.test(snils) && snils.length !== 0) {
          return this.errors.push('Неверный формат СНИЛС');
        }

        let splitedSnils = snils.split('').filter(val => /[0-9]/.test(val));

        if (splitedSnils.length !== 11) {
          this.errors.push('Неверная длина СНИЛС');
        }

        let controlNumber = splitedSnils.slice(-2).join('');
        let snilsNumbers = splitedSnils.slice(0, -2);

        let sumSnilsNumbers = snilsNumbers.reverse().reduce(function(sum, current, index) {
          return sum + current * (index + 1);
        }, 0);

        if (sumSnilsNumbers < 100 && sumSnilsNumbers.toString() !== controlNumber) {
          this.errors.push('Введен неверный СНИЛС');
        }

        else if ((sumSnilsNumbers === 100 || sumSnilsNumbers === 101) && controlNumber !== '00') {
          this.errors.push('Введен неверный СНИЛС');
        }

        else if (sumSnilsNumbers > 101) {
          let remainder = sumSnilsNumbers % 101;
          if ((remainder === 100 || remainder === 101) && controlNumber !== '00') {
            this.errors.push('Введен неверный СНИЛС');
          }

          else if (remainder < 100 && remainder.toString() !== controlNumber) {
            this.errors.push('Введен неверный СНИЛС');
          }
        }
      },
      sendData() {
        axios.post('https://localhost:5001/api/patient', this.postBody)
          .then((response) => {
            console.log(response);
            this.postBody = {
              "firstName": null,
              "lastName": null,
              "middleName": null,
              "birthDate": null,
              "gender": null,
              "SNILS": null
            }
          })
          .catch((error) => {
            console.log(error);
          });
      }
    }
  }
</script>

<style lang="scss">
  .input-section {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
  }

  .input-field {
    margin-bottom: 10px;
    width: 40%;
    line-height: 19px;
    border: 1px solid #868686;
    border-radius: 50px;

    &:focus {
      outline: 0;
      border-color: #66d1c6;
    }

    &__name {
      padding: 6px 22px 8px 22px;
      font-size: 18px;
    }
  }

  input[type="date"] {
    padding-left: 15px;
    font-size: 18px;
    line-height: 20px;
    border: 1px solid #868686;
    border-radius: 50px;

    & + img {
      position: absolute;
      width: 24px;
      height: 20px;
      left: 344px;
      top: 4px;
    }
  }

  input[type="date"]::-webkit-calendar-picker-indicator {
    opacity: 0;
    z-index: 100;
    padding: 7px;
    cursor: pointer;
  }

  .select {
    display: flex;
    justify-content: space-between;

    & .date-birthday {
      position: relative;
      display: flex;
      justify-content: space-between;
      width: 370px;

      & > p {
        display: inline-block;
        margin: 0;
      }
    }

    & .gender {
      display: flex;
      justify-content: space-between;
      width: 370px;
      align-items: center;

      & > p {
        display: inline-block;
        margin: 0;
      }

      & > label {
        line-height: 18px;
      }
    }
  }

  input[type="radio"] {
    margin-left: 15px;
  }

  .button-submit {
    margin: 30px auto 11px;
    padding-top: 5px;
    padding-bottom: 6px;
    display: flex;
    justify-content: center;
    width: 165px;
    font-size: 18px;
    line-height: 25px;
    color: #fff;
    background-color: #66d1c6;
    border-radius: 50px;
    border: 1px solid #66d1c6;
    cursor: pointer;
  }

  .errors {
    margin: 0;

    & > ul {
      margin: 0;
    }
  }
</style>