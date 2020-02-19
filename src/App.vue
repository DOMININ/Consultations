<template>
  <div id="app" class="panel">
    <h2 class="panel-title">Учетная система "Консультации"</h2>
    <ul class="panel-list">
      <li
        v-for="tab in tabs"
        v-bind:key="tab.eng"
        v-on:click="currentTab = tab.eng"
        v-on:keyup.enter="currentTab = tab.eng"
        class="panel-item"
      >
        <input
          class="visually-hidden panel-radio"
          type="radio"
          name="tab"
          :id="tab.eng"
        />
        <label :for="tab.eng">{{ tab.rus }}</label>
      </li>
    </ul>

    <keep-alive>
      <component v-bind:is="currentTabComponent" />
    </keep-alive>
  </div>
</template>

<script>
  import Patients from "./components/Patients/Patients";
  import Consultations from "./components/Consultations/Consultations";

  export default {
    components: {Patients, Consultations},
    data() {
      return {
        currentTab: "Patients",
        tabs: [
          {
            eng: "Patients",
            rus: "Пациенты"
          },
          {
            eng: "Consultations",
            rus: "Консультации"
          }
        ]
      };
    },
    computed: {
      currentTabComponent: function() {
        return this.currentTab;
      }
    },
    mounted() {
      document.querySelector('[name=tab]:first-child').checked = true;
    }
  }
</script>

<style lang="scss">
  .panel {
    position: absolute;
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    display: flex;
    flex-direction: column;
    margin: auto;
    padding: 50px 50px 50px 50px;
    width: 1030px;
    min-height: 65vh;
    background-color: #fff;
    border-radius: 15px;

    &-title {
      margin: 0 auto 40px;
      font-size: 30px;
      line-height: 24px;
      text-align: center;
    }

    &-list {
      display: flex;
      justify-content: center;
      margin: 0;
      padding: 0;
      list-style: none;
    }

    &-item {
      &:first-child .panel-radio + label {
        border-radius: 5px 0 0 5px;
      }
      &:last-child .panel-radio + label {
        border-radius: 0 5px 5px 0;
      }
    }

    &-radio + label {
      display: block;
      padding: 13px 0 13px 0;
      width: 150px;
      text-align: center;
      font-size: 18px;
      line-height: 24px;
      border: 1px solid #ccc;
      cursor: pointer;
      color: #626262;
    }

    &-radio + label:hover {
      color: #ed462f;
      border-color: #ed462f;
    }

    &-radio[type="radio"]:checked + label {
      background: #ed462f;
      border-color: #ed462f;
      color: #fff;
    }

    &-radio[type="radio"]:focus + label {
      outline-offset: -2px;
      outline: rgb(77, 144, 254) auto 5px;
    }
  }
</style>

